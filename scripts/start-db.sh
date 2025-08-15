docker run -d --name cemiyet-db \
  -e POSTGRES_USER=cemiyet \
  -e POSTGRES_PASSWORD=cemiyet \
  -e POSTGRES_DB=cemiyet \
  -p 5432:5432 \
  -v postgres-data:/var/lib/postgresql/data \
  --restart unless-stopped \
  postgres:latest