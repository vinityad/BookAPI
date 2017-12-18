# BookAPI

WebApi application with Entity Framework (code first) with the following endpoints.
- GET /books - get all books
- GET /book/{id} - get book by id
- DELETE /book/{id} - delete book (should not delete item in DB, only mark it is deleted)
- POST /book/{id} - create book
- PATCH /book/{id} - update book (partial, update only fields which was sent)

``` csharp
Book object
{
  id: string, // guid
  name: string,
  numberOfPages: number,
  dateOfPublication: number, // utc timestamp
  createDate: number, // utc timestamp, internal only (not returned by api)
  updateDate: number, // utc timestamp, internal only (not returned by api)
  authors: string[]
}
``` 

Also it uses following technology.
* Autofac
* AutoMapper
* EF6
* JsonPatch
* Swagger
* Repository Pattern
* Unit of Work