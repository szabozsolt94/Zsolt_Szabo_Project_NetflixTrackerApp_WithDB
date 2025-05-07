$(document).ready(function () {

    // Save the API url
    let apiUrl = "http://localhost:62481/api/NetflixMovies";

    // Call the NetflixTrackerAPI and fetch every movie
    $.ajax({
        url: apiUrl,
        type: "GET",
        success: function (movies) {
            // Loop through each movie and create a card for it
            for (let i = 0; i < movies.length; i++) {
                let movie = movies[i];

                // Create the movie card
                // Set data attributes for the filtering dropdown and to identify the movie
                let card = $('<div class="card">');
                card.attr('data-type', movie.Type.toLowerCase());
                card.attr('data-movie-id', movie.MovieID);

                // Create the title and append it to the card
                let title = $('<h3>').text(movie.Title + " (" + movie.Year + ")");
                card.append(title);

                // Create move image and append it to the card
                let img = $('<img>');
                img.attr('src', movie.Image ? 'data:image/jpeg;base64,' + movie.Image : '/img/moviecardplaceholder.jpg');
                img.attr('alt', movie.Title);
                card.append(img);

                // Create a container for the 3 icons
                let iconContainer = $('<div class="icon-container">');

                // Create and add the "Favorites" icon
                let favIcon = $('<i class="fa-regular fa-star icon-button favorite">');
                favIcon.click(function () {
                    $(this).toggleClass('fa-solid fa-regular');
                    let movieId = $(this).closest('.card').data('movie-id');
                    addMovieToFavorites(movieId);
                });

                // Create and add the "Watched" icon
                let watchedIcon = $('<i class="fa-regular fa-circle-check icon-button watched">');
                watchedIcon.click(function () {
                    $(this).toggleClass('fa-solid fa-regular');
                    let movieId = $(this).closest('.card').data('movie-id');
                    addMovieToWatched(movieId);
                });

                // Create and add the "Watch Later" icon
                let laterIcon = $('<i class="fa-regular fa-clock icon-button watch-later">');
                laterIcon.click(function () {
                    $(this).toggleClass('fa-solid fa-regular');
                    let movieId = $(this).closest('.card').data('movie-id');
                    addMovieToWatchLater(movieId);
                });

                // Append icons to the icon container
                iconContainer.append(favIcon, watchedIcon, laterIcon);
                card.append(iconContainer);

                // Append the card to the grid
                $('#moviesGrid').append(card);
            }
        },
        error: function () {
            alert("Error fetching movie data.");
        }
    });
});

// Function to handle click event on "Favorites" (star) icon
// Send MovieID and UserID to the backend API to add or remove the movie from Favorites DB table
function addMovieToFavorites(movieId) {
    $.ajax({
        url: 'https://localhost:7017/favorites/add',
        type: 'POST',
        data: {
            movieID: movieId,
            userID: 1
        },
        success: function (response) {
            alert(response.message);
            let favIcon = $('.card[data-movie-id=' + movieId + '] .favorite');

            // Toggle the look of the icon when adding / removing movies as favorites
            if (response.message === "Movie added to Favorites!") {
                favIcon.addClass('fa-solid').removeClass('fa-regular');
            } else if (response.message === "Movie removed from Favorites!") {
                favIcon.addClass('fa-regular').removeClass('fa-solid');
            }
        },
        error: function () {
            alert("Error adding movie to favorites.");
        }
    });
}

// Function to handle click event on "Watched" icon
// Send MovieID and UserID to the backend API to add or remove the movie from Watched DB table
function addMovieToWatched(movieId) {
    $.ajax({
        url: 'https://localhost:7017/watched/add',
        type: 'POST',
        data: {
            movieID: movieId,
            userID: 1
        },
        success: function (response) {
            alert(response.message);
            let watchedIcon = $('.card[data-movie-id=' + movieId + '] .watched');

            // Toggle the look of the icon when adding / removing movies as favorites
            if (response.message === "Movie marked as Watched!") {
                watchedIcon.addClass('fa-solid').removeClass('fa-regular');
            } else if (response.message === "Movie removed from Watched!") {
                watchedIcon.addClass('fa-regular').removeClass('fa-solid');
            }
        },
        error: function () {
            alert("Error marking movie as watched.");
        }
    });
}

// Function to handle click event on "Watch Later" icon
// Send MovieID and UserID to the backend API to add or remove the movie from WatchLater DB table
function addMovieToWatchLater(movieId) {
    $.ajax({
        url: 'https://localhost:7017/watchlater/add',
        type: 'POST',
        data: {
            movieID: movieId,
            userID: 1
        },
        success: function (response) {
            alert(response.message);
            let watchLaterIcon = $('.card[data-movie-id=' + movieId + '] .watchlater');

            // Toggle the look of the icon when adding / removing movies as favorites
            if (response.message === "Movie added to Watch Later!") {
                watchLaterIcon.addClass('fa-solid').removeClass('fa-regular');
            } else if (response.message === "Movie removed from Watch Later!") {
                watchLaterIcon.addClass('fa-regular').removeClass('fa-solid');
            }
        },
        error: function () {
            alert("Error updating Watch Later list.");
        }
    });
}

// Search bar functionality on Home page
$('#movieSearch').on('input', function () {
    let searchText = $(this).val().toLowerCase();

    // Loop through the title of each movie card
    $('.card').each(function () {
        let movieTitle = $(this).find('h3').text().toLowerCase();

        // Check if the title includes the search letter or text
        if (movieTitle.includes(searchText)) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
});

// Filter dropdown functionality on Home page
$('#filterDropdown').on('change', function () {
    let selectedOption = $(this).val();

    // Loop through each movie card and check their data-type (movie or series)
    $('.card').each(function () {
        let cardType = $(this).data('type');

        // Show all cards if nothing is selected
        if (selectedOption === '') {
            $(this).show();
        }

        // Show only movies
        else if (selectedOption === 'movies' && cardType === 'movie') {
            $(this).show();
        }

        // Show only series
        else if (selectedOption === 'tvshows' && cardType === 'series') {
            $(this).show();
        }

        // Hide anything that doesn't match
        else {
            $(this).hide();
        }
    });
});



