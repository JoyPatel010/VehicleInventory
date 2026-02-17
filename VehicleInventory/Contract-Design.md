Contract-First API Design
Request and response structure was prescribed before deploying the API to make it clear and consistent.

API Versioning Strategy
The API has versioning based on URL:

/api/v1/vehicles

This will enable new versions to be allocated without breaking the existing clients.



Create Vehicle
Endpoint: POST /api/v1/vehicles
Request body:

{
  "vehicleCode": "CAR001",
  "locationId": "TORONTO",
  "vehicleType": "SUV"
}


Validation rules:
vehicleCode is required
locationId is required
vehicleType is required

Responses:
201 Created – vehicle successfully created
400 Bad Request – invalid input



Get Vehicle
Endpoint: GET /api/v1/vehicles/{id}
Response:

{
  "id": "8f14e45f-ea25-4c3a-b9e5-6f9a3b1e9c77",
  "vehicleCode": "CAR001",
  "locationId": "TORONTO",
  "vehicleType": "SUV",
  "status": 0
}

Responses:
200 OK – vehicle found
404 Not Found – vehicle does not exist




Update Vehicle Status
Endpoint: PUT /api/v1/vehicles/{id}/status
Request body:

{
  "status": 1
}

Validation rules:
Status must be a valid enum value
Invalid lifecycle transitions are rejected by the Domain layer

Responses:
204 No Content – status updated
400 Bad Request – invalid transition
404 Not Found – vehicle not found



Delete Vehicle
Endpoint: DELETE /api/v1/vehicles/{id}

Responses:
204 No Content – vehicle deleted
404 Not Found – vehicle not found


This contract-first style made sure that the entire inputs, output, rule of validation, as well as status codes before implementation were defined.