document.addEventListener('DOMContentLoaded', () => {
    const registerForm = document.getElementById('registerForm');
    const passwordInput = document.getElementById('password');
    const passwordStrength = document.getElementById('passwordStrength');
    const formMessages = document.getElementById('formMessages');

    // Real-time password strength indicator
    passwordInput.addEventListener('input', () => {
        const strength = getPasswordStrength(passwordInput.value);
        passwordStrength.textContent = `Password Strength: ${strength}`;
    });

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

            const data = await response.json();
            if (response.ok) {
                formMessages.textContent = data.message;
            } else {
                formMessages.textContent = `Error: ${data.message}`;
            }
        } catch (error) {
            console.error('Error during registration:', error);
            formMessages.textContent = 'An error occurred while registering. Please try again.';
        }
    });

    function getPasswordStrength(password) {
        const lengthCriteria = password.length >= 8;
        const uppercaseCriteria = /[A-Z]/.test(password);
        const numberCriteria = /\d/.test(password);
        const specialCharCriteria = /[!@#$%^&*(),.?":{}|<>]/.test(password);

        const strength = [lengthCriteria, uppercaseCriteria, numberCriteria, specialCharCriteria]
            .filter(Boolean)
            .length;

        switch (strength) {
            case 4:
                return 'Strong';
            case 3:
                return 'Medium';
            case 2:
                return 'Weak';
            default:
                return 'Very Weak';
        }
    }
});
