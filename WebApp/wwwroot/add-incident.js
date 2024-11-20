document.addEventListener('DOMContentLoaded', () => {
    const incidentForm = document.getElementById('incidentForm');
    const formMessages = document.getElementById('formMessages');

    incidentForm.addEventListener('submit', async (e) => {
        e.preventDefault();

        const incidentData = {
            title: document.getElementById('title').value,
            description: document.getElementById('description').value,
            severity: document.getElementById('severity').value,
            status: document.getElementById('status').value,
            assignedUser: document.getElementById('assignedUser').value
        };

        try {
            const response = await fetch('http://localhost:5242/api/Incident', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(incidentData),
            });

            const data = await response.json();
            formMessages.textContent = response.ok ? 'Incident added successfully' : `Error: ${data.message}`;
        } catch (error) {
            console.error('Error:', error);
            formMessages.textContent = 'An error occurred. Please try again.';
        }
    });
});
