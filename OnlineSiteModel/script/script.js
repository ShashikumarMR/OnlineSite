document.querySelector(".filterIcon").addEventListener("click", () => {
    document.querySelector(".mask").style.display = "block";
    document.querySelector(".filter").style.display = "block";
});

document.querySelector(".mask").addEventListener("click", () => {
    document.querySelector(".mask").style.display = "none";
    document.querySelector(".filter").style.display = "none";
});


