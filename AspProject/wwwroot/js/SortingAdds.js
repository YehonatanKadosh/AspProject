$("#SortByTitle").on("click", () => {
    var list, i, switching, b, shouldSwitch;
    list = document.getElementById("AdvertsList");
    switching = true;
    /* Make a loop that will continue until
    no switching has been done: */
    while (switching) {
        // Start by saying: no switching is done:
        switching = false;
        b = list.getElementsByTagName("LI");
        // Loop through all list items:
        for (i = 0; i < (b.length - 1); i++) {
            // Start by saying there should be no switching:
            shouldSwitch = false;
            /* Check if the next item should
            switch place with the current item: */
            if (b[i].children.item(1).children.item(0).children.item(0).children.item(0).children.item(0).children.item(0).innerHTML.toLowerCase() >
                b[i + 1].children.item(1).children.item(0).children.item(0).children.item(0).children.item(0).children.item(0).innerHTML.toLowerCase()) {
                /* If next item is alphabetically lower than current item,
                mark as a switch and break the loop: */
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            /* If a switch has been marked, make the switch
            and mark the switch as done: */
            b[i].parentNode.insertBefore(b[i + 1], b[i]);
            switching = true;
        }
    }
    $("#SortByTitle").removeClass("btn-outline-primary")
        .addClass("btn-primary");
    $("#SortByDate").removeClass("btn-primary")
        .addClass("btn-outline-primary");
});

$("#SortByDate").on("click", () => {
    var list, i, switching, b, shouldSwitch;
    list = document.getElementById("AdvertsList");
    switching = true;
    /* Make a loop that will continue until
    no switching has been done: */
    while (switching) {
        // Start by saying: no switching is done:
        switching = false;
        b = list.getElementsByTagName("LI");
        // Loop through all list items:
        for (i = 0; i < (b.length - 1); i++) {
            // Start by saying there should be no switching:
            shouldSwitch = false;
            /* Check if the next item should
            switch place with the current item: */
            if (new Date(b[i].children.item(1).children.item(0).children.item(0).children.item(0).children.item(0).children.item(2).innerHTML) >
                new Date(b[i + 1].children.item(1).children.item(0).children.item(0).children.item(0).children.item(0).children.item(2).innerHTML)) {
                /* If next item is alphabetically lower than current item,
                mark as a switch and break the loop: */
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            /* If a switch has been marked, make the switch
            and mark the switch as done: */
            b[i].parentNode.insertBefore(b[i + 1], b[i]);
            switching = true;
        }
    }
    $("#SortByDate").removeClass("btn-outline-primary")
        .addClass("btn-primary");
    $("#SortByTitle").removeClass("btn-primary")
        .addClass("btn-outline-primary");
});
