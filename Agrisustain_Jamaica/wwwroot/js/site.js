const locationIcon = document.querySelector(".location-icon")

const searchIcon = document.querySelector(".search-icon")
const weatherHeader = document.querySelector(".weather-header")

//// Get user's geolocation
function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        x.innerHTML = "Geolocation is not supported by this browser.";
    }
}

async function showPosition(position) {
    const { latitude, longitude } = position.coords;

     // Create a JSON object with the location data

    const locationData = { latitude, longitude };

      //Send location data to the server
    try {
        const response = await fetch('/weatherforecast/weatherforecast', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(locationData)
        });
        if (response.ok) {
            // Location data sent successfully
            console.log('Location data sent to server');
        } else {
            // Handle error
            console.error('Failed to send location data to server');
        }
    } catch (error) {
        console.error('Error sending location data:', error);
    }

}

//Event Listeners
getLocation()
locationIcon.addEventListener("click", () => {
    
})


searchIcon.addEventListener("click", () => {
    const afterElement = window.getComputedStyle(
        document.querySelector('.weather-header'), '::after');

        afterElement.style.display = "block"
})
