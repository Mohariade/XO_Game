const xoMatrix=document.getElementById("xo-matrix");

//testing the acces
console.log(xoMatrix.nodeName);
console.log(xoMatrix.children.length);

//perfuming the insert of X nad O when clicking any of cells

for (let i=0; i<9; i++){
    var cell=xoMatrix.children[i];
    cell.addEventListener("click", function() {
        alert("cell "+ (i+1)+ " clicked");


        let x=document.createElement("h1", "X");
        cell.appendChild(x);
        
    })
}
