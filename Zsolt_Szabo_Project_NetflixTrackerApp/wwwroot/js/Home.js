// Fetch all the Netflix movies from the API
$(document).ready(function () {

    // Set the API endpoint URL
    var apiUrl = "http://localhost:62481/api/NetflixMovies";

    // Make a GET request to the API
    $.ajax({
        url: apiUrl,
        type: "GET",

        success: function (movies) {
            movies.forEach(function (movie) {
                console.log(movie.Title, movie.Type);

                // Create a new card for each movie
                var $card = $('<div>', {
                    class: 'card',
                    'data-movie-id': movie.MovieID // Store MovieID in a data attribute
                });

                // Create the movie title with year
                var $title = $('<h3>').text(`${movie.Title} (${movie.Year})`);

                // Create the movie image
                var $img = $('<img>', {
                    src: `data:image/jpeg;base64,${movie.Image}`,
                    alt: movie.Title
                });

                // Create container for icons
                var $iconContainer = $('<div>', { class: 'icon-container' });

                // "Add to Favorites" icon
                var $favIcon = $('<i>', {
                    class: 'fa-regular fa-star icon-button favorite'
                }).on('click', function () {
                    $(this).toggleClass('fa-solid fa-regular');

                    // Get the MovieID from the clicked card
                    var movieId = $(this).closest('.card').data('movie-id');

                    // Add to favorites logic (call a function to handle this)
                    addMovieToFavorites(movieId);
                });

                // "Mark as Watched" icon
                var $watchedIcon = $('<i>', {
                    class: 'fa-regular fa-circle-check icon-button watched'
                }).on('click', function () {
                    $(this).toggleClass('fa-solid fa-regular');
                });

                // "Add to Watch Later" icon
                var $laterIcon = $('<i>', {
                    class: 'fa-regular fa-clock icon-button watch-later'
                }).on('click', function () {
                    $(this).toggleClass('fa-solid fa-regular');
                });

                // Append icons to the container
                $iconContainer.append($favIcon, $watchedIcon, $laterIcon);

                // Append title, image, and icons to the card
                $card.append($title, $img, $iconContainer);

                // Append the card to the grid
                $('#moviesGrid').append($card);
            });
        },

        error: function () {
            alert("Error fetching movie data.");
        }
    });
});

// Add movie to favorites function
// Add movie to favorites or remove it if already in favorites
function addMovieToFavorites(movieId) {
    $.ajax({
        url: 'https://localhost:7017/favorites/add',  // Full URL to the route
        type: 'POST',
        data: {
            movieID: movieId,
            userID: 1  // Assuming UserID = 1 for now
        },
        success: function (response) {
            alert(response.message); // Show success message

            // After the request, toggle the class for the favorite icon
            const $favIcon = $(`.card[data-movie-id=${movieId}] .favorite`);

            if (response.message === "Movie added to Favorites!") {
                $favIcon.addClass('fa-solid').removeClass('fa-regular');  // Change to solid star
            } else if (response.message === "Movie removed from Favorites!") {
                $favIcon.addClass('fa-regular').removeClass('fa-solid');  // Change to regular star
            }
        },
        error: function () {
            alert("Error adding movie to favorites.");
        }
    });
}








// Search functionality
$('#movieSearch').on('input', function () {
    var searchText = $(this).val().toLowerCase(); // Change text to lower case for case-insensitivity

    // Loop through all movie cards
    $('.card').each(function () {
        var title = $(this).find('h3').text().toLowerCase();

        // If title contains the search text, show the card; otherwise, hide it
        if (title.includes(searchText)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
});


// Filtering dropdown menu by type (movies or TV shows)
$('#filterDropdown').on('change', function () {
    var filter = $(this).val(); // Get the selected value

    // Loop through all movie cards, check their types
    $('.card').each(function () {
        var isMovie = $(this).data('type') === 'movie';   
        var isTVShow = $(this).data('type') === 'series';

        // Show or hide the card based on the selected filter
        if (filter === '') {
            $(this).show(); 
        }
        else if (filter === 'movies' && isMovie) {
            $(this).show();
        }
        else if (filter === 'tvshows' && isTVShow) {
            $(this).show();
        }
        else {
            $(this).hide();
        }
    });
});


