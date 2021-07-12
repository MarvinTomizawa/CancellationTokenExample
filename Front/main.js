// async function GetPokemonsFetch() {
//     document.getElementById("carregamento").innerHTML = "Carregando...";
//     try {
//         const response = await fetch("http://localhost:5000/pokemon?canTimeout=true", {
//             mode: "no-cors"
//         });

//         console.log(response);

//         document.getElementById("resultado").innerHTML = JSON.stringify(response);

//     } catch (error) {
//         document.getElementById("resultado").innerHTML = error;

//     } finally {
//         document.getElementById("carregamento").innerHTML = "";
//     }
// }

function GetPokemonXml() {
    const canCancelRequest = document.getElementById("cancell").checked;
    
    document.getElementById("carregamento").innerHTML = "Carregando...";
    document.getElementById("resultado").innerHTML = "";

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById("resultado").innerHTML = xhttp.responseText;
            document.getElementById("carregamento").innerHTML = "";
        }
    };
    xhttp.open("GET", 'http://localhost:5000/pokemon?canTimeout=' + canCancelRequest, true);
    xhttp.send();
}


document.getElementById("chamar-request").onclick = GetPokemonXml;