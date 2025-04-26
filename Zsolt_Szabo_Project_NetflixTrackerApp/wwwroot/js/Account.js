document.addEventListener("DOMContentLoaded", () => {
    const editIcons = document.querySelectorAll(".edit-icon");

    editIcons.forEach(icon => {
        icon.addEventListener("click", () => {
            const field = icon.closest(".account-field");
            const span = field.querySelector("span");
            const input = document.createElement("input");

            input.type = "text";
            input.value = span.innerText.trim();
            input.classList.add("form-control");
            field.replaceChild(input, span);

            // Hide the pencil icon when clicked
            icon.classList.add("hidden");

            // Add ✔ and X icons for Save and Cancel
            const saveIcon = document.createElement("i");
            const cancelIcon = document.createElement("i");

            saveIcon.classList.add("fa", "fa-check-circle", "save-icon");
            cancelIcon.classList.add("fa", "fa-times-circle", "cancel-icon");

            field.appendChild(saveIcon);
            field.appendChild(cancelIcon);

            // Handle Save icon click
            saveIcon.addEventListener("click", () => {
                console.log("Save clicked");

                const newValue = input.value.trim();
                const fieldName = field.querySelector("label").innerText.replace(":", "");

                fetch("/Home/UpdateAccount", {  
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({
                        userId: document.querySelector("#UserID").value,
                        fieldName,
                        newValue
                    })
                })
                    .then(response => {
                        if (response.ok) {
                            span.innerText = newValue;
                            field.replaceChild(span, input);
                            saveIcon.remove();
                            cancelIcon.remove();

                            // Show the pencil icon again after saving
                            icon.classList.remove("hidden");
                        } else {
                            alert("Error updating profile.");
                        }
                    });
            });

            // Handle Cancel icon click
            cancelIcon.addEventListener("click", () => {
                field.replaceChild(span, input);  // Restore the original value (span)
                saveIcon.remove();  // Remove the save icon
                cancelIcon.remove();  // Remove the cancel icon

                // Show the pencil icon again after canceling
                icon.classList.remove("hidden");
            });
        });
    });
});
