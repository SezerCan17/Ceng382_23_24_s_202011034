
var clickCount = 0; 

function buttonFunc() {
    clickCount++; 
    

    
    if (clickCount == 1) { 
        alert("İlk tıklama!"); 
    } else if (clickCount == 2) { 
        document.getElementById("Hello").style.display = "none";
    }
}
