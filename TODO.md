### TODO LIST

**_Bugs to fix_**

- [ ] **fix(Workflow):** ![HIGH][high] limit IAM user to only permissions necessary to create/manage roles
  - _**Security/IAM:** ensure created roles have specific permissions for their respective tasks_
  - _**Security/IAM:** use specific ARNs in policies rather than "\*" to limit scope of permissions_
  - _**Security/Cleanup:** implement proper cleanup of unused resources to prevent accumulation_
- [ ] **fix(Demo/Todo):** ![MID][mid] nullreferenceexception in marshallrequest method when handling api gateway requests with "APIGatewayHttpApiV2ProxyFunction" in LambdaEntryPoint.cs
- [ ] **fix(Workflow):** ![MID][mid] build warnings about "--output" option not being supported when building a solution in the GitHub workflow

**_New features to add_**

- [ ] **add(demo/[solutions]):** unit test
- [ ] **add(demo/[solution])**: ASP.NET Core demo solution API
- [ ] **add(Workflow):** ![MID][mid] publish workflow to GitHub Actions Marketplace

#### IN PROGRESS

-

#### DONE

-

[high]: https://img.shields.io/badge/-HIGH-red
[mid]: https://img.shields.io/badge/-MID-yellow
[low]: https://img.shields.io/badge/-LOW-green
