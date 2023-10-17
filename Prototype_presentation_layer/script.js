const xoMatrix=document.getElementById("xo-matrix");

//testing the acces
console.log(xoMatrix.nodeName);
console.log(xoMatrix.children.length);


//choosing X or O for the player
const openantProfile=document.getElementsByClassName("profile-card")[0];

const randNumber=Math.floor(Math.random() * 2);

console.log(randNumber);
let mychoice, openantChoice;

if (randNumber==0){
    mychoice="O";
    openantChoice="X";
} else {
    mychoice="X";
    openantChoice="O";
}



const myChoiceElem=document.createElement("h1")
const openantChoiceElem=document.createElement("h1");

myChoiceElem.textContent=mychoice;
openantChoiceElem.textContent=openantChoice;

//Adding choices
openantProfile.insertAdjacentElement("beforeend", openantChoiceElem);

const headerElem=document.getElementById("header-container");
const user=document.getElementById("profile");
user.prepend(myChoiceElem);


//perfuming the insert of X nad O when clicking any of cells
for (let i=0; i<9; i++){
    let cell=xoMatrix.children[i];

    function cellClickHandeler() {
        
        let x=document.createElement("h1");
        x.textContent=mychoice;

        cell.insertAdjacentElement('afterbegin', x);

        console.log("Element added succefully!");

        cell.removeEventListener("click", cellClickHandeler)
        
    }

    cell.addEventListener("click", cellClickHandeler);
}
