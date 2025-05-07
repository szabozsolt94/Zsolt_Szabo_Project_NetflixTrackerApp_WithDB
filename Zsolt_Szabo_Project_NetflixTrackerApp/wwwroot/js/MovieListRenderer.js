function renderMovieList(movieIDs, containerId) {
    const apiBaseUrl = 'http://localhost:62481';

    movieIDs.forEach(movieId => {
        fetch(`${apiBaseUrl}/api/NetflixMovies/${movieId}`)
            .then(response => {
                if (!response.ok) throw new Error("Movie not found");
                return response.json();
            })
            .then(movie => {
                const $card = $('<div>', {
                    class: 'card',
                    'data-type': movie.Type.toLowerCase(),
                    'data-movie-id': movie.MovieID
                });

                const $title = $('<h3>').text(`${movie.Title} (${movie.Year})`);
                const $img = $('<img>', {
                    src: movie.Image
                        ? `data:image/jpeg;base64,${movie.Image}`
                        : '/img/moviecardplaceholder.jpg',
                    alt: movie.Title
                });

                $card.append($title, $img);
                $(`#${containerId}`).append($card);
            })
            .catch(err => console.error("Error:", err.message));
    });
}
