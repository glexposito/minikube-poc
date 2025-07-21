# Build stage
FROM rust:1.88-alpine as builder
WORKDIR /app
RUN apk add --no-cache musl-dev
COPY node_id_api/ .
RUN cargo build --release

# Runtime stage
FROM alpine:3.20
RUN apk add --no-cache ca-certificates
COPY --from=builder /app/target/release/node_id_api /usr/local/bin/node_id_api
EXPOSE 8081
CMD ["node_id_api"]
