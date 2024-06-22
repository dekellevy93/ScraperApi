# IMDb Actor Scraper API

## Description

This C# .NET Core API scrapes actor information from IMDb's "Top Actors Around the World" list (https://www.imdb.com/list/ls054840033/) and provides a RESTful interface for managing an in-memory database of actors. 

## Features

- **Web Scraping:** Extracts actor data (name, rank, bio, best movie) from IMDb using HtmlAgilityPack and Selenium for handling dynamically loaded content.
- **RESTful API:**  Provides endpoints for managing actors (listing, retrieving, adding, updating, deleting).
- **Filtering and Pagination:** Allows filtering actors by name and rank range, and supports pagination for retrieving large lists.
- **Error Handling and Validation:** Includes robust error handling and input validation to ensure data integrity.
- **Design Patterns and Architecture:** Utilizes appropriate design patterns (e.g., Repository Pattern, Dependency Injection) and separates concerns (Data Access, Business Logic, API).

## Getting Started

**Prerequisites:**

- .NET 6 SDK (or later)
- Visual Studio, Visual Studio Code, or your preferred IDE

## API Endpoints

**1. Retrieve a List of Actors:**

   - **Endpoint:** `/api/actors/{pageNumber}/{pageSize}`
   - **Method:** `GET`
   - **Path Parameters:**
     - `pageNumber`: The page number to retrieve (required).
     - `pageSize`: The number of actors per page (required).
   - **Query Parameters:**
      - `nameFilter` (optional): Filter actors by name containing the provided string.
      - `minRank` (optional): Filter actors with a rank greater than or equal to this value.
      - `maxRank` (optional): Filter actors with a rank less than or equal to this value.
   - **Sample Request:**
      ```
      GET /api/actors/2/20?nameFilter=Chaplin&minRank=1&maxRank=10
      ```
   - **Sample Response:**
      ```json
      [
        {
          "id": 1,
          "name": "Charles Chaplin"
        },
        {
          "id": 2,
          "name": "Marlon Brando"
        },
        // ... more actors
      ]
      ```

**2. Get Actor Details:**

   - **Endpoint:** `/api/Actor/{id}`
   - **Method:** `GET`
   - **Path Parameter:**
      - `id`: The ID of the actor to retrieve.
   - **Sample Request:**
      ```
      GET /api/Actor/1
      ```
   - **Sample Response:**
      ```json
      {
        "id": 1,
        "name": "Charles Chaplin",
        "rank": 1,
        "bio": "Considered to be one of the most...",
        "bestMovie": "The Great Dictator (1940)"
      }
      ```

**3. Add a New Actor:**

   - **Endpoint:** `/api/Actor`
   - **Method:** `POST`
   - **Request Body:**
      ```json
      {
        "name": "New Actor Name",
        "rank": 100,
        "bio": "New actor's bio...",
        "bestMovie": "New Actor's Best Movie (Year)"
      }
      ```
   - **Sample Response (Success):**
      ```
      201 Created 
      ```

**4. Update Actor Details:**

   - **Endpoint:** `/api/Actor/{id}`
   - **Method:** `PUT`
   - **Path Parameter:**
      - `id`: The ID of the actor to update.
   - **Request Body:**
      ```json
      {
        "id": 1,
        "name": "moishe",
        "rank": 1,
        "bio": "string",
        "bestMovie": "string"
      }
      ```
   - **Sample Response (Success):**
      ```
      200 Success
      ```

**5. Delete an Actor:**

   - **Endpoint:** `/api/Actor/{id}`
   - **Method:** `DELETE`
   - **Path Parameter:**
      - `id`: The ID of the actor to delete.
   - **Sample Request:**
      ```
      DELETE /api/Actor/1
      ```
   - **Sample Response (Success):**
      ```
      200 Success
      ```

## Error Handling

The API provides appropriate HTTP status codes and error messages for various error conditions, including:

- `400 Bad Request`: For invalid input data.
- `404 Not Found`: When an actor with the specified ID is not found.
- `500 Internal Server Error`:  For unexpected errors during scraping or database operations.

## Design and Architecture

- **Repository Pattern:** Used to abstract data access logic.
- **Dependency Injection:**  Implemented to decouple components and improve testability.
- **Separate Layers:** The project is structured with clear separation between the data access layer, business logic layer, and API layer. 
