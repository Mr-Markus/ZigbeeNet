# CI & CD setup

Continuous Integration (CI) and Continuous Delivery (CD)
has been implemented using GitHub Actions. 
Content on these files can be found in `.github/workflows/` folder.

CI uses `dotnet` command-line tooling in build steps.
Build is initiated by `push` to branch or `pull request`
to `develop` or `master` branches.

CD also uses `dotnet` tooling. It's initiated by
creating GitHub Release in the repository. 

**Important**: CD uses `tag_name` from the release
as it's package version. Meaning if you tag your
release using `v1.5.0`, it will automatically version NuGet
packages as `1.5.0`.

**Important 2**: CD requires `NUGET_API_KEY` names [secret](https://docs.github.com/en/actions/reference/encrypted-secrets)
to be stored into the repository secrets.
It's used when publishing NuGet Packages to the nuget.org.
