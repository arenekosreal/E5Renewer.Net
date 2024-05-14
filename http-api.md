`/v1/list_apis`

Get all msgraph api names supported

    HTTP Method: GET

    Returns: Invoke result [^1]

`/v1/running_users`

Get all running users

    HTTP Method: GET

    Returns: Invoke result [^1]

`/v1/waiting_users`

Get all waiting users

    HTTP Method: GET

    Returns: Invoke result [^1]

`/v1/user_results`

Get api calling results of user given

    HTTP Method: GET

    Query params:

        `user`: The user name defined in configuration

        `api_name`: The api name

    Returns: Invoke result [^1]

[^1] Invoke result is json data like this:
```jsonc
{
    "method": "<method>", // The method you called
    "args": {...}, // The arguments to the method in json format
    "result": ..., // The result of method invoked
    "timestamp": <timestamp> // Generated from server, milisecond timestamp
}
```
