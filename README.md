# MS365 E5 Subscription Renewer

## What is this

A tool to renew e5 subscription by calling msgraph APIs

## How to use
1. Create Application

    See [Register Application](https://learn.microsoft.com/graph/auth-register-app-v2) and [Configure Permissions](https://learn.microsoft.com/graph/auth-v2-service#2-configure-permissions-for-microsoft-graph) for more info. We need `tenant id`, `client id` and `client secret` of your application to access msgraph APIs.

2. Create Configuration

    Copy [`config.json.example`](./config.json.example) to `config.json`, edit it as your need. You can always add more credentials. Please edit `auth_token` so only people you authenticated can access the statistics.
    We will listen on tcp socket by default, if `listen_addr` is an empty string and your platform supports Unix domain socket, we will listen on Unix domain socket with path `listen_socket` and permission `listen_socket_permission`.

    Tips: We support json, yaml and toml formats, just let their contents be equal, the configuration result is same.

3. Install Python

    See [here](https://www.python.org/about/gettingstarted/) for more info, we need python 3.11 and later.

4. Get program

    You can download prebuilt program at [Release](https://github.com/arenekosreal/e5renewer/releases) page, install the wheel by running `pip install /path/to/wheel.whl`.
    If you want to build from source, you can follow those steps:
    1. Install poetry

        You can see [here](https://python-poetry.org/docs/) for more info.

    2. Install dependencies

        Run `poetry install` in the repository, if you want to make contributions, use `poetry install --with=dev` to install extra development dependencies.

    3. Build wheel

        Run `poetry build` in the repository, you will get wheel at `dist` folder.

5. Run program

    After you installed wheel, there should be a new command `e5renewer` available. Run `e5renewer -h` to see help.
    If you want to run from source, simply run `poetry run e5renewer` in the repository.

## Get running statistics

Using `curl` or any tool which can send http request, send request to `http://<listen_addr>:<listen_port>` or unix socket `<listen_socket>`,
each request should be sent with header `Authentication: <auth_token>`.
You will get json response if everything is fine. If it is a GET request, send milisecond timestamp in query param `timestamp`,
If it is a POST request, send milisecond timestamp in post json with key `timestamp`.

For example:

<details>

<summary>HTTP API for /v1/list_apis</summary>

```
curl -H 'Authentication: <auth_token>' -H 'Accept: application/json' \
    'http://<listen_addr>:<listen_port>/v1/list_apis?timestamp=<timestamp>' | jq '.'
{
    "method": "list_apis",
    "args": {},
    "result": [
        "AgreementAcceptances.Get",
        "Admin.Get",
        "Agreements.Get",
        "AppCatalogs.Get",
        "ApplicationTemplates.Get",
        "Applications.Get",
        "AuditLogs.Get",
        "AuthenticationMethodConfigurations.Get",
        "AuthenticationMethodsPolicy.Get",
        "CertificateBasedAuthConfiguration.Get",
        "Chats.Get", "Communications.Get",
        "Compliance.Get",
        "Connections.Get",
        "Contacts.Get",
        "DataPolicyOperations.Get",
        "DeviceAppManagement.Get",
        "DeviceManagement.Get",
        "Devices.Get",
        "Direcory.Get",
        "DirectoryObjects.Get",
        "DirectoryRoleTemplates.Get",
        "DirectoryRoles.Get",
        "DomainDnsRecords.Get",
        "Domains.Get",
        "Drives.Get",
        "Education.Get",
        "EmployeeExperience.Get",
        "External.Get",
        "FilterOperators.Get",
        "Functions.Get",
        "GroupLifecyclePolicies.Get",
        "GroupSettingTemplates.Get",
        "GroupSetings.Get",
        "Groups.Get",
        "Identity.Get",
        "IdentityGovernance.Get",
        "IdentityProtection.Get",
        "IdentityProviders.Get",
        "InfomationProtecion.Get",
        "Invitations.Get",
        "OAuth2PermissionGrants.Get",
        "Organization.Get",
        "PermissionGrants.Get",
        "Places.Count.Get",
        "Places.GraphRoom.Get",
        "Planner.Get",
        "Policies.Get",
        "Print.Get",
        "Privacy.Get",
        "Reports.Get",
        "RoleManagement.Get",
        "SchemaExtensions.Get",
        "ScopedRoleMemberships.Get",
        "Search.Get",
        "Security.Get",
        "ServicePrincipals.Get",
        "Shares.Get",
        "Sites.Get",
        "Solutions.Get",
        "SubscribedSkus.Get",
        "Subscriptions.Get",
        "Teams.Get",
        "TeamsTemplates.Get",
        "Teamwork.Get",
        "TenantRelationships.Get",
        "Users.Get"
    ],
    "timestamp": "<timestamp_returned_by_server>"
}

```
</details>

Server will only accept request less than **30 seconds** older than server time.

See [docs/API.md](./docs/API.md) for possible apis

**Note:** This program supports **HTTP** only, and it is insecure. Please use a reverse proxy tool like `nginx` or `apache` in front of it to transfer data through untrusted environment.

## References

[msgraph-sdk-python](https://github.com/microsoftgraph/msgraph-sdk-python)

[aiohttp](https://github.com/aio-libs/aiohttp)

[pytest](https://github.com/pytest-dev/pytest)

[pytest-cov](https://github.com/pytest-dev/pytest-cov)

[pytest-asyncio](https://github.com/pytest-dev/pytest-asyncio)

[pytest-aiohttp](https://github.com/aio-libs/pytest-aiohttp)

[pytest-ruff](https://github.com/businho/pytest-ruff)

[![Ruff](https://img.shields.io/endpoint?url=https://raw.githubusercontent.com/astral-sh/ruff/main/assets/badge/v2.json)](https://github.com/astral-sh/ruff)

[Kate](https://kate-editor.org/)

[pyright](https://github.com/microsoft/pyright)

and many other dependencies in [poetry.lock](poetry.lock)