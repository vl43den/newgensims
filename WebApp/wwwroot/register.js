document.addEventListener('DOMContentLoaded', () => {
    const registerForm = document.getElementById('registerForm');

    registerForm.addEventListener('submit', async (e) => {
        e.preventDefault();

        const username = document.getElementById('username').value;
        const name = document.getElementById('name').value;
        const email = document.getElementById('email').value;
        const password = document.getElementById('password').value;

        const registerData = {
            username: username,
            name: name,
            email: email,
            password: password,
        };

        try {
            const response = await fetch('http://localhost:5242/api/user/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(registerData),
            });

            if (response.ok) {
                const data = await response.json();
                alert(data.message);
            } else {
                const error = await response.json();
                alert(`Error: ${error.message}`);
            }
        } catch (error) {
            console.error('Error during registration:', error);
            alert('An error occurred while registering. Please try again.');
        }
    });
});
