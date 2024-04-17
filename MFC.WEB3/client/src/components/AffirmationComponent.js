import React, { useState, useEffect } from 'react';
import axios from 'axios';

const AffirmationComponent = () => {
    const [affirmation, setAffirmation] = useState('');

    useEffect(() => {
        const fetchAffirmation = async () => {
            try {
                const result = await axios('https://localhost:7082/Affirmation/Daily');
                setAffirmation(result.data.message);
            } catch (error) {
                console.error('Error fetching daily affirmation:', error);
                setAffirmation('No affirmation available today.');
            }
        };

        fetchAffirmation();
    }, []);

    return (
        <div>
            <h1>Daily Affirmation</h1>
            <p>{affirmation}</p>
        </div>
    );
};

export default AffirmationComponent;
