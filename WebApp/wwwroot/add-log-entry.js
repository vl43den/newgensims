document.addEventListener('DOMContentLoaded', () => {
    const logEntryForm = document.getElementById('logEntryForm');
    const formMessages = document.getElementById('formMessages');

    logEntryForm.addEventListener('submit', async (e) => {
        e.preventDefault();

        const logEntryData = {
            timestamp: document.getElementById('timestamp').value,
            logLevel: document.getElementById('logLevel').value,
            message: document.getElementById('message').value
        };

        try {
            const response = await fetch('http://localhost:5242/api/LogEntry', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(logEntryData),
            });

            const data = await response.json();
            formMessages.textContent = response.ok ? 'Log entry added successfully' : `Error: ${data.message}`;
        } catch (error) {
            console.error('Error:', error);
            formMessages.textContent = 'An error occurred. Please try again.';
        }
    });
});
