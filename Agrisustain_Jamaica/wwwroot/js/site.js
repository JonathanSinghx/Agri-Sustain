//const locationIcon = document.querySelector(".location-icon")

//const searchIcon = document.querySelector(".search-icon")
//const weatherHeader = document.querySelector(".weather-header")

////// Get user's geolocation
//function getLocation() {
//    if (navigator.geolocation) {
//        navigator.geolocation.getCurrentPosition(showPosition);
//    } else {
//        x.innerHTML = "Geolocation is not supported by this browser.";
//    }
//}

//async function showPosition(position) {
//    const { latitude, longitude } = position.coords;

//     // Create a JSON object with the location data

//    const locationData = { latitude, longitude };

//      //Send location data to the server
//    try {
//        const response = await fetch('/weatherforecast/weatherforecast', {
//            method: 'POST',
//            headers: {
//                'Content-Type': 'application/json'
//            },
//            body: JSON.stringify(locationData)
//        });
//        if (response.ok) {
//            // Location data sent successfully
//            console.log('Location data sent to server');
//        } else {
//            // Handle error
//            console.error('Failed to send location data to server');
//        }
//    } catch (error) {
//        console.error('Error sending location data:', error);
//    }

//}

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
