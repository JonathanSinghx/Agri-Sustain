
//weather trigger Form control
const changeElements = document.querySelectorAll("[data-name]");

changeElements.forEach(el => el.addEventListener("change", (e) => {
    let text;
    if (e.target.getAttribute("data-name") == "weatherCondition") {
        text = e.target.options[e.target.selectedIndex].value;
        document.querySelector("#weatherVal").textContent = text;
    } else if (e.target.getAttribute("data-name") == "greater-or-less-than") {
        if (e.target.options[e.target.selectedIndex].value.includes("<")) {
            text = "is less than";
            document.querySelector("#conditionVal").textContent = text;
        }
    } else if (e.target.getAttribute("data-name") == "level") {
        text = e.target.value;
        document.querySelector("#levelVal").textContent = text;
    } else if (e.target.getAttribute("data-name") == "units") {
        text = e.target.options[e.target.selectedIndex].value;
        document.querySelector("#unitVal").textContent = text;
    } else if (e.target.getAttribute("data-name") == "duration") {
        text = e.target.options[e.target.selectedIndex].value;
        document.querySelector("#durationVal").textContent = text;
    }
}))


