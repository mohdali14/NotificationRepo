//string clientId = "550224"; // 550224,441562,551383

////string hubUrl = "https://localhost:7173/hubs/policy?clientId={clientId}";
//string hubUrl = "https://localhost:7173/hubs/policy";

////string hubUrl = "https://localhost:7173/hubs/policy";
//var hubConnection = new HubConnectionBuilder()
//    .WithUrl(hubUrl)
//    .Build();

//hubConnection.On<Notification>("ReceiveClientUpdates", (policyUpdates) =>
//{
//    Console.WriteLine($"Message received: {policyUpdates.Message}");
//});

//try
//{
//    // Start the connection
//    hubConnection.StartAsync().Wait();
//    Console.WriteLine("SignalR connection started.");
//    //await hubConnection.InvokeAsync("JoinClientGroup", clientId);
//    _ = Task.Run(() => hubConnection.InvokeAsync("JoinClientGroup", clientId));

//}
//catch (Exception ex)
//{
//    Console.WriteLine($"Error connecting to SignalR: {ex.Message}");
//    throw;
//}
////Create a cancellation token to stop the connection
//CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
////hubConnection.StopAsync().Wait();
//var cancellationToken = cancellationTokenSource.Token;
//// Handle Ctrl+C to gracefully shut down the application
//Console.CancelKeyPress += (sender, a) =>
//{
//    a.Cancel = true;
//    Console.WriteLine("Stopping SignalR connection...");
//    cancellationTokenSource.Cancel();
//};
//try
//{
//    // Keep the application running until it is cancelled
//    await Task.Delay(Timeout.Infinite, cancellationToken);
//}
//catch (TaskCanceledException)
//{
//}
//// Stop the connection gracefully
//await hubConnection.StopAsync();

Console.WriteLine("SignalR connection closed.");
