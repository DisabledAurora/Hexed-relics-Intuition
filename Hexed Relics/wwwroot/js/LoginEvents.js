function fieldIsEmpty(str) {
    return !str.trim().length;
}


document.querySelector("#un").onchange = function () {
    if (fieldIsEmpty(document.querySelector("#un").value) && fieldIsEmpty(document.querySelector("#pw").value)) {
        document.querySelector("#signupbtn").disabled = false;
    }
    else {
        document.querySelector("#signupbtn").disabled = true;
    }
}

document.querySelector("#pw").onchange = function () {
    console.log("change pw");
    if (fieldIsEmpty(document.querySelector("#un").value) && fieldIsEmpty(document.querySelector("#pw").value))
        document.querySelector("#signupbtn").disabled = false;
    else
        document.querySelector("#signupbtn").disabled = true;
}