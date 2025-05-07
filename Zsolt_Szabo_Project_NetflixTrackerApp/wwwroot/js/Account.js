document.addEventListener("DOMContentLoaded", function () {
    // Select all edit icons
    const editIcons = document.querySelectorAll(".edit-icon");

    // Loop through all the icons
    for (let i = 0; i < editIcons.length; i++) {
        const icon = editIcons[i];

        icon.addEventListener("click", function () {
            // Get the closest field to the icon
            const field = icon.closest(".account-field");
            const span = field.querySelector("span");

            // Create an input field to replace the span
            const input = document.createElement("input");
            input.type = "text";
            input.value = span.innerText.trim();
            input.classList.add("form-control");

            // Replace span with input field
            field.replaceChild(input, span);

            // Hide the pencil icon
            icon.classList.add("hidden");

            // Create Save and Cancel icons using Font Awesome
            const saveIcon = document.createElement("i");
            const cancelIcon = document.createElement("i");

            saveIcon.classList.add("fa", "fa-check-circle", "save-icon");
            cancelIcon.classList.add("fa", "fa-times-circle", "cancel-icon");

            field.appendChild(saveIcon);
            field.appendChild(cancelIcon);

            // Click event for Save icon
            saveIcon.addEventListener("click", function () {

                // Get the new value from the input field, remove whitespace and ":" from the field name
                const newValue = input.value.trim();
                const fieldName = field.querySelector("label").innerText.replace(":", "");

                // Send POST request to the server
                fetch("/Account/UpdateAccount", {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({
                        userId: document.querySelector("#UserID").value,
                        fieldName: fieldName,
                        newValue: newValue
                    })
                })
                    .then(function (response) {
                        if (response.ok) {
                            // Update the UI if successful
                            span.innerText = newValue;
                            field.replaceChild(span, input);
                            saveIcon.remove();
                            cancelIcon.remove();
                            icon.classList.remove("hidden");
                        } else {
                            alert("Error updating profile.");
                        }
                    });
            });

            // Click event for Cancel icon
            cancelIcon.addEventListener("click", function () {
                // Restore the original values
                field.replaceChild(span, input);
                saveIcon.remove();
                cancelIcon.remove();
                icon.classList.remove("hidden");
            });
        });
    }
});