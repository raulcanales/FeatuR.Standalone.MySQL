# FeatuR Standalone (MySQL)

This is a basic implementation for the package FeatuR.EntityFramework.MySQL, where it stores the data for the features in a MySQL database, and exposes them throug several endpoints.

## Consumer endpoints

All of the endpoints will fetch **all the headers** from the request, and pass it to the system as `IFeatureContext`. Some good applications for this, could be passing:

- User id
- Client version
- IP Address
- Region

This are the different endpoints that the API offers.

### Is enabled
```
GET /api/feature/{featureId}/isenabled
```

This endpoint will return `true` or `false`, depending if the feature should be enabled for the given context.

### Evaluate features

```
POST /api/features/evaluate

Request:
['feature-1', 'feature-2', 'feature-3']

Response:
{
   'feature-1': true,
   'feature-2': true,
   'feature-3': false
}
```

With this endpoint, we can pass a list of feature ids, and the API will return a dictionary containing the same ids, and a boolean indicating wether they are enabled or not for the given context.

### List enabled features

```
GET /api/features

Response:
['feature-1', 'feature-2', 'feature-3']
```

This endpoint will just give us a list with the ids of the features enabled for the given context.
