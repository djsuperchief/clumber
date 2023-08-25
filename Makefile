SHELL :=/usr/bin/env bash

run:
	@dotnet run --project src/clumber

run-release:
	@dotnet run --project src/clumber -c Release

run-benchmark:
	@dotnet run --project src/clumber -c Release -- -b

run-test:
	@dotnet run --project src/clumber -c Debug -- -t ./TestPacks
