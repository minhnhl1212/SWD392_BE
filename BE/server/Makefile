start:
	dotnet watch --launch-profile dev

build:
	dotnet build

test:
	dotnet test

docker:
	docker build -t nshop -f Dockerfile .

new-migration:
	dotnet ef migrations add $(name) --verbose 

migration:
	dotnet ef database update

.PHONY: start build docker new-migration migration test