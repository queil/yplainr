module Server

open Saturn
open Config

let endpointPipe = pipeline {
    plug head
    plug requestId
}

let app = application {
    pipe_through endpointPipe

    error_handler (fun ex _ -> pipeline { 
        //render_html (InternalError.layout ex) 
        json {|error=500; details=ex.Message|}
        })
    use_router Router.appRouter
    url "http://0.0.0.0:8085/"
    memory_cache
    use_static "static"
    use_gzip
    use_config (fun _ -> {placeholder = None} ) //TODO: Set development time configuration
}

[<EntryPoint>]
let main _ =
    printfn "Working directory - %s" (System.IO.Directory.GetCurrentDirectory())
    run app
    0 // return an integer exit code