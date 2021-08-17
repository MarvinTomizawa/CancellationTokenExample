# Cancellation token dentro do .Net

Hoje em dia utilizamos bastante recursos assincronos como async/await, Tasks, configureAwait entre outros em endpoints e consoles/background services para podermos rodar instruções paralelamente em threads separadas e evitar bloqueios de recursos que demorem demais. Mas uma das funcionalidades legais para poder salvar um pouco os recursos da máquina(Memória/Processamento/Conexões ao banco de dados) como por exemplo o CancellationToken as vezes pode ser ignorada por só não saber como utilizar deste recurso.

A utilização correta de cancellation tokens pode ajudar bastante a liberar recursos, como por exemplo, algumas threads que são utilizadas pelo servidor no momento de uma request, ou algumas conexões no banco de dados, estas que não serão mais utilizadas e podem ser canceladas evitando gastos de processamento e IO. Esse simples cancelamento pode até economizar um dinheiro dependendo se a aplicação está hospedada em algum ambiente cloud que cobra por processamento/IO, ou evitar que o servidor fique lento processando diversas requisições que não serão mais utilizadas.

## Um exemplo onde se cancelar request é vantajoso

## Mas então como uma funcionalidade assíncrona é cancelada?
Depende bastante de onde se está sendo utilizado recursos assincronos, alguns exemplos são:

- Um processo rodando em uma x quantidade de vezes a cada y quantidade de vezes ou rodando infinitamente de acordo com algum loop onde será informado que deverá ser cancelado após x tempo ou quando for inserido algum input do usuário para cancelar essa execução.
- Uma requisição GET na web que foi disparada quando o usuário entra em uma tela para listar alguns dados, mas que deve ser cancelada quando o usuário mudar de página e não precisar mais dos dados daquele GET.
- Um input do usuário para cancelar uma funcionalidade dentro de uma aplicação que faz um processamento paralelo e deixa o usuário livre para realizar outras ações enquanto finaliza em background.
- Um limite de tempo para realizar uma operação uma vez, ou fazer uma maneira de retentativa por x quantidade de tempo.

Da para ver que existem diversas formas de se cancelar uma request e tratar assíncronamente uma funcionalidade, mas antes de vermos como podemos cancelar uma request é legal conhecer alguns métodos e objetos. 

## Objetos e métodos que é interessante conhecer
- __CancellationTokenSource__ = um objeto que cria um cancellation token, também é utilizado para poder cancelar o token que ele gerou
    - __CancellationTokenSource.Token__ = Pega o CancellationToken criado pelo CancellationTokenSource
    - __CancellationTokenSource.Cancel__ = Altera o status do CancellationToken gerado pelo TokenSource para cancelado.
    - __CancellationTokenSource.CancelAfter__ = Altera o status do CancellationToken gerado pelo TokenSource para cancelado após um x periodo de tempo informado em milisegundos.
    - __CancellationTokenSource.Dispose__ = É sempre recomendado utilizar o cancelationTokenSource dentro de um using ou realizar um dispose nele para evitar que se reutilize de outros tokens com dados incorretos, podendo iniciar operações com tokens já cancelados por execuções passadas.


- __CancellationToken__ = Um objeto que é passado para um ou mais parametros que deseja ser cancelado caso o token não esteja mais válido, não é possível chamar um método nele para se cancelar, quem cancela o token é apenas o CancellationTokenSource
    - __CancellationToken.IsCancellationRequested__ = Método utilizado para verificar se o status do token está cancelado (Este método é chamado dentro de libs que utilizam recursos async e normalmente estouram alguma exception para poder cancelar o fluxo de execução antes de se utilizar de um recurso, um exemplo são consultas dentro do entity que ao verem que a request está cancelada chama o método ThrowIfCancellationRequested)
    - __CancellationToken.CanBeCanceled__ = É um atributo que permite que o CancellationTokenSource faça alterações no status do cancellationToken
    - __CancellationToken.ThrowIfCancellationRequested__ = É a maneira recomendada de se parar um fluxo caso o status do cancellation token seja cancelado, assim ele invoca uma exception (OperationCanceledException)informando que a request foi cancelada.
    - __CancellationToken.None__ = É um token que não pode ser cancelado, a propriedade CanBeCanceled é false, também é o valor default de um CancellationToken

- __OperationCanceledException__ = Algumas bibliotecas retornar essa exception quando tentam realizar alguma operação com uma Task cancelada
- __TaskCanceledException__ = Algumas bibliotecas retornar essa exception quando tentam realizar alguma operação com uma Task cancelada (Herda de OperationCanceledException)

## Como tratar as exceptions canceladas?
Como é possivel a interrupção do fluxo de execução utilizando exceptions sempre é bom ter uma maneira de trata-las evitando que chegue ao usuário final, as maneiras de tratá-las é igual uma exception normal. Pode apenas se utilizar de um try/catch para tratar em cenários específicos ou trata-los nos middlewares.

Depende bastante do objetivo que se tem dentro da funcionalidade a ser utilizada, se for um endpoint que apenas mostraria alguns dados, ter essa task cancelada não afeta muito e apenas loga-lo seria o suficiente (Não é muito interessante ignorar tasks canceladas pois acaba-se perdendo dados importantes para o sistema para que no futuro possa ser utilizado para insights de performance ou melhorias em telas onde o usuário perde a paciencia e fica dando muitos f5).

Agora se fosse um processamento pesado onde o usuário teria de esperar um grande processamento de dados que possa ser complicado de montar, adicionar um comportamento baseado em por exemplo um [dead letter queue](https://www.ibm.com/docs/en/ibm-mq/9.0?topic=components-dead-letter-queues) para os dados que foram informados caso o usuário cancele a operação pode ajudar depois para dar um suporte ao usuário caso o mesmo continue tendo problemas.

## Como utilizar o cancellation token em aplicações asp.net core? (Controllers)
Nos controllers é possível utilizar os cancellation tokens sem precisar vc mesmo cria-los utilizando o cancellationTokenSource, basta pega-los dos parametros de seus métodos GET/POST/PUT/DELETE e o controller que fica como responsável de cancelar o token. Assim caso o usuário saia da página que realizou um request ou manualmente cancele a request (Usando cancell no postman, ou cancelando mesmo utilizando fetch/axios/qualquer outra ferramenta de request) o próprio controller irá manipular o CancellationTokenSource para cancelar a request

## Como utilizar o cancellation token em consoles e background services

Nos consoles e background service pode se utilizar novos tokens para gerar dados em periodos definidos de tempo, ou limitar quantidades de retentativas de consultas a outros recursos em um período de tempo. Também é possível a utlização de Tasks rodando em background para capturar um input do usuário em alguma tabela ou no próprio console mesmo e utilizar deste input para cancelar o processamento.

## Quais são os problemas a utilizar cancellationToken
### 1 Inconsistencia de dados em cenários que não cuidam de transações
Se o cancellation token for utilizado em cenários onde aconteçam alterações de dados em mais de uma fonte de dado pode ser que ocorra inconsistencia dos mesmos se não for tratado as transações corretamente.

[CRIAR_UM_EXEMPLO_PRATICO_AQUI]
Um exemplo é ao ser realizado um POST para persistir dados de uma nova compra salva no banco 1, mas antes de salvar no banco 2 é cancelado a request. Como já foi salvo no banco 1, porém não foi salvo no banco 2 vai dar merda.