//// Get user's geolocation
//navigator.geolocation.getCurrentPosition(async (position) => {
//    const { latitude, longitude } = position.coords;

//    // Create a JSON object with the location data
//    const locationData = { latitude, longitude };

//    // Send location data to the server
//    try {
//        const response = await fetch('/api/LocationEndpoint', {
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
//});


const searchIcon = document.querySelector(".search-icon")
const weatherHeader = document.querySelector(".weather-header")
searchIcon.addEventListener("click", () => {
    const afterElement = window.getComputedStyle(
        document.querySelector('.weather-header'), '::after');

        afterElement.style.display = "block"
})
