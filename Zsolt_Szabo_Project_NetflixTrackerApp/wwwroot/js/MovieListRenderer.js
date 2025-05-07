function renderMovieList(movieIDs, containerId) {

    // Save the url for NetflixTrackerAPI
    const apiBaseUrl = 'http://localhost:62481';

    // Loop through each movieID in the movieIDs array
    movieIDs.forEach(movieId => {
        // Fetch movie data from API using movieId
        fetch(`${apiBaseUrl}/api/NetflixMovies/${movieId}`)
            .then(response => {
                if (!response.ok) {
                    // If the response is not OK, throw an error
                    throw new Error("Movie not found");
                }
                // Convert response to JSON if successful
                return response.json();
            })
            .then(movie => {
                // Create a new movie card container
                const card = document.createElement('div');
                card.classList.add('card');
                card.dataset.type = movie.Type.toLowerCase();
                card.dataset.movieId = movie.MovieID;

                // Add movie title
                const title = document.createElement('h3');
                title.textContent = `${movie.Title} (${movie.Year})`;

                // Add movie image
                const img = document.createElement('img');
                img.src = movie.Image
                    ? `data:image/jpeg;base64,${movie.Image}`
                    : '/img/moviecardplaceholder.jpg';
                img.alt = movie.Title;

                // Append title and image to the card
                card.appendChild(title);
                card.appendChild(img);

                // Append the card to the container
                const container = document.getElementById(containerId);
                container.appendChild(card);
            })
            .catch(err => {
                // Log any errors that occurred during the fetch for debugging
                console.error("Error:", err.message);
            });
    });
}

