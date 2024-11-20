document.addEventListener('DOMContentLoaded', () => {
    const userForm = document.getElementById('userForm');
    const toggleFormButton = document.getElementById('toggleForm');
    const formTitle = document.getElementById('formTitle');
    const nameGroup = document.getElementById('nameGroup');
    const emailGroup = document.getElementById('emailGroup');
    const formMessages = document.getElementById('formMessages');

    let isLogin = false;

    toggleFormButton.addEventListener('click', () => {
        isLogin = !isLogin;
        if (isLogin) {
            formTitle.textContent = 'Login';
            nameGroup.style.display = 'none';
            emailGroup.style.display = 'none';
            toggleFormButton.textContent = 'Switch to Register';
        } else {
            formTitle.textContent = 'Register';
            nameGroup.style.display = 'block';
            emailGroup.style.display = 'block';
            toggleFormButton.textContent = 'Switch to Login';
        }
    });

    userForm.addEventListener('submit', async (e) => {
        e.preventDefault();

        const username = document.getElementById('username').value;
        const password = document.getElementById('password').value;
        const name = isLogin ? null : document.getElementById('name').value;
        const email = isLogin ? null : document.getElementById('email').value;

        const url = isLogin
            ? 'http://localhost:5242/api/user/login'
            : 'http://localhost:5242/api/user/register';

        const requestData = isLogin
            ? { username, password }
            : { username, name, email, password };

        try {
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(requestData),
            });

            const data = await response.json();
            formMessages.textContent = response.ok ? data.message : `Error: ${data.message}`;
        } catch (error) {
            console.error('Error:', error);
            formMessages.textContent = 'An error occurred. Please try again.';
        }
    });
});
