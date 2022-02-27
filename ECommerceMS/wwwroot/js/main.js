const allDeleteBtns = document.querySelectorAll(".delete-item");

allDeleteBtns.forEach((btn) => {
    btn.addEventListener("click", (e) => {
        let confirmResult = confirm("Are You Want To Delete That?");
        if (!confirmResult)
            e.preventDefault();
    });
})