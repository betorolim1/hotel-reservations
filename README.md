# Hotel Reservation

This is an API to reserve a room in hotel. 

# EndPoints:
##  GET: /users
Retrieves all users on database. 
  
##  GET: /users/{userId}/reservations
Retrieves all the user reservations.

##  PUT: users/{userId}/reservations/{reservationId}
Updates the user reservation.

##  POST: users/{userId}/reservations
Creates an user reservation.

##  DELETE: users/{userId}/reservations/{reservationId}
Cancels the user reservation.

##  GET: reservations/in-period
Retrieves all reservations in the informated period.

## Obs
1. There is an Middleware responsable for logging the errors of the application in the folder "Logs".
2. There is already an user on database for testing purposes. Use "GET: /users" endpoint to get the user id required in most of the endpoints.
3. Entityframework in-memory is used to database access, so if the application restart, it will save no data (except for the testing user).
4. Swagger is available (https://localhost:xxxxx/swagger/index.html).
