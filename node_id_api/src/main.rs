use actix_web::{get, App, HttpServer, Responder, HttpResponse};
use serde::Serialize;
use std::env;

#[derive(Serialize)]
struct NodeIdResponse {
    node_id: String,
}

#[get("/node-id")]
async fn node_id() -> impl Responder {
    let node_id = env::var("HOSTNAME").unwrap_or_else(|_| {
        hostname::get()
            .unwrap_or_default()
            .to_string_lossy()
            .to_string()
    });

    HttpResponse::Ok().json(NodeIdResponse { node_id })
}

#[get("/healthz")]
async fn healthz() -> impl Responder {
    // Equivalent to Predicate = _ => false; (i.e., no checks, just respond OK)
    HttpResponse::Ok().body("Healthy")
}

#[get("/ready")]
async fn readiness() -> impl Responder {
    // Simulate readiness check — e.g. DB reachable, etc.
    let is_ready = true;

    if is_ready {
        HttpResponse::Ok().body("Ready")
    } else {
        HttpResponse::ServiceUnavailable().body("Not Ready")
    }
}

#[actix_web::main]
async fn main() -> std::io::Result<()> {
    HttpServer::new(|| {
        App::new()
            .service(node_id)
            .service(healthz)
            .service(readiness)
    })
    .bind(("0.0.0.0", 8081))?
    .run()
    .await
}
