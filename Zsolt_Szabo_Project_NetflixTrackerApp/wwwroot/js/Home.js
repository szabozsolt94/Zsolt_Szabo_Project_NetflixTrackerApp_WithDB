// Fetch all the Netflix movies from the API
$(document).ready(function () {

    // Set the API endpoint URL
    var apiUrl = "http://localhost:62481/api/NetflixMovies";

    // Make a GET request to the API
    $.ajax({
        url: apiUrl,
        type: "GET",

        // If successful, loop through the movies and create cards
        success: function (movies) {
            movies.forEach(function (movie) {
                var $card = $('<div>', { class: 'card' });
                var $title = $('<h3>').text(`${movie.Title} (${movie.Year})`);
                var $img = $('<img>', {
                    src: `data:image/jpeg;base64,${movie.Image}`,
                    alt: movie.Title
                });

                // When the image has fully loaded, add the 'loaded' class to trigger CSS effects (this was needed due to issue with CSS not applying to images)
                $img.on('load', function () {
                    $(this).addClass('loaded');
                });

                $card.append($title).append($img);
                $('#moviesGrid').append($card);
            });
        },

        // If the request fails
        error: function () {
            alert("Error fetching movie data.");
        }
    });
});
