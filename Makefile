SHELL :=/usr/bin/env bash

run:
	@dotnet run --project src/clumber

run-release:
	@dotnet run --project src/clumber --property:Configuration=Release

run-benchmark: run-release
