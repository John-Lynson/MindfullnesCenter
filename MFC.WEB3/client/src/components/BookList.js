import React, { useState, useEffect } from 'react';
import axios from 'axios';

const BookList = () => {
    const [books, setBooks] = useState([]);

    useEffect(() => {
        const fetchBooks = async () => {
            const result = await axios('/books');
            setBooks(result.data);
        };
        fetchBooks();
    }, []);

    return (
        <div>
            <h1>Books on Mindfulness and Spiritual Growth</h1>
            <ul>
                {books.map(book => (
                    <li key={book.id}>{book.title} - {book.author}</li>
                ))}
            </ul>
        </div>
    );
};

export default BookList;
