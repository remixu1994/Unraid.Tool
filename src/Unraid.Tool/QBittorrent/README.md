# qbittorrent API Documentation

This is a RESTful API to interact with qbittorrent using a C# backend. The API provides functionalities like logging in, retrieving torrents, and adding, editing, or deleting trackers.

## Base URL


## Endpoints

- **Login**
    - **Method:** `POST`
    - **Endpoint:** `/api/qbittorrent/Login`
    - **Description:** Logs into the qbittorrent instance with provided credentials.
    - **Request Body:**
      ```json
      {
        "Host": "http://localhost:8080",
        "UserName": "your_username",
        "Password": "your_password"
      }
      ```
    - **Response:**
      ```json
      {
        "message": "Login successfully."
      }
      ```

- **Get All Torrents**
    - **Method:** `GET`
    - **Endpoint:** `/api/qbittorrent/torrents`
    - **Description:** Retrieves all torrents from the qbittorrent instance.

- **Add Trackers**
    - **Method:** `POST`
    - **Endpoint:** `/api/qbittorrent/add-trackers/{torrentHash}`
    - **Description:** Adds trackers to a specific torrent.
    - **Request Body:**
      ```json
      "tracker_url"
      ```

- **Edit Tracker**
    - **Method:** `POST`
    - **Endpoint:** `/api/qbittorrent/edit-tracker/{torrentHash}`
    - **Description:** Edits the URL of an existing tracker for a torrent.
    - **Request Body:**
      ```json
      {
        "OrigUrl": "original_tracker_url",
        "NewUrl": "new_tracker_url"
      }
      ```

- **Replace Trackers**
    - **Method:** `POST`
    - **Endpoint:** `/api/qbittorrent/replace-tracker`
    - **Description:** Replaces all original trackers with a new URL across all torrents.
    - **Request Body:**
      ```json
      {
        "OrigUrl": "original_tracker_url",
        "NewUrl": "new_tracker_url"
      }
      ```

- **Delete Tracker**
    - **Method:** `DELETE`
    - **Endpoint:** `/api/qbittorrent/delete-tracker/{torrentHash}`
    - **Description:** Removes a tracker from a specific torrent.
    - **Query Parameter:**
        - `trackerUrl`: The tracker URL to remove.

## Error Handling

- `200 OK`: The request was successful.
- `400 Bad Request`: The user is not authenticated or the request is malformed.
- `404 Not Found`: The requested resource could not be found (e.g., invalid torrent hash).

## Notes

- Authentication is required before accessing any of the other endpoints. Use the `Login` endpoint to authenticate.
- Ensure tracker URLs are properly encoded when sent in requests.
