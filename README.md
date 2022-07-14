This repository aims to present a CRUD using MongoDB and Asp.Net.

## Docker use

To build and run only api with docker you can use these commands in the same folder as the **Dockerfile** file :

    docker build --rm -t myapp .

    docker run --rm -p 5000:5000 myapp

Or you can use Docker Compose with the command:
    
    docker compose up