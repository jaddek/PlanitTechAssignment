# Readme

https://github.com/jaddek/PlanitTechAssignment

## Installation

1. Install Playwright browsers (run once):
   pwsh TechnicalAssessmentTests/bin/Debug/net9.0/playwright.ps1 install

2. Restore dependencies and build your project:
   dotnet restore
   dotnet build

3. Run tests:
   dotnet test TechnicalAssessmentTests/TechnicalAssessmentTests.csproj --no-build --logger trx
   (or if you have a Makefile with test target, run: make test)

## Using Make file

### Help for makefile

```
make help
```

### Run install + tests

```bash
make run
```

### Run tests

```bash
make tests
```

## Pipelines

Configuration file for:
- github actions (tested, publically available)
- jenkins (not tested as don't have a host and agents on jenkins)

