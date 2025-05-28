DOTNET=dotnet
TEST_PROJECT=TechnicalAssessmentTests/TechnicalAssessmentTests.csproj

.PHONY: help install-playwright restore build test test-filter test-debug clean run

help:
	@echo "Available targets:"
	@echo "  install-playwright      - Install Playwright browsers (run once)"
	@echo "  restore                 - Restore .NET dependencies"
	@echo "  build                   - Build the project"
	@echo "  test                    - Run all tests, headless, no build, output TRX logger"
	@echo "  test-local              - Run all tests, headless off, no build, output TRX logger"
	@echo "  test-filter             - Run filtered tests (use FILTER variable), no build, TRX logger"
	@echo "  test-debug              - Run tests with diagnostics, no build, no restore"
	@echo "  clean                   - Clean the project"
	@echo "  run                     - Run install-playwright, restore, build, and test"

install-playwright:
	pwsh TechnicalAssessmentTests/bin/Debug/net9.0/playwright.ps1 install

restore:
	$(DOTNET) restore

build:
	$(DOTNET) build

test:
	$(DOTNET) test $(TEST_PROJECT) --no-build --logger trx; 

test-local:
	$(DOTNET) test $(TEST_PROJECT) --settings ./TechnicalAssessmentTests/local.runsettings --no-build --logger trx; 


test-filter:
	$(DOTNET) test $(TEST_PROJECT) --no-build --logger trx --filter "$(FILTER)"

test-debug:
	$(DOTNET) test $(TEST_PROJECT) --no-build --no-restore --logger trx -- --diagnostics $(DEBUG_ARGS)

clean:
	$(DOTNET) clean

run: install-playwright restore build test
