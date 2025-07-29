# Unity Asset Register SDK

**An Asset Register client plugin for Unity, by [Futureverse](https://www.futureverse.com)**

The **Asset Register SDK** is a Unity Engine plugin for creating and sending GraphQL requests to the **Asset Register**. It provides a flexible interface for constructing queries, handling responses, and integrating with Futureverse asset data.

> See https://ar-docs.futureverse.app/ for more specific details on the Asset Register.

> For more on the broader Futureverse developer ecosystem, visit the [Futureverse Documentation Hub](https://docs.futureverse.com).

## Installation

Go to the Unity Package Manager window, and select `Add package from git URL...` and enter this link https://github.com/futureversecom/sdk-unity-asset-register.git?path=Assets/Plugins/AssetRegister (append `#vX.X.X` to specify a version). Alternatively, you can get a .unitypackage from the Releases page.

To use it in your project, you will need to include the AssetRegister.Runtime Assembly Definition.

### UniTask Compatibility

The Asset Register SDK uses asynchronous methods to make web requests. It uses coroutines, so you must call yield return when using them. But through use of Version Defines, if you have [UniTask](https://github.com/Cysharp/UniTask) installed, it will change the signature of those asynchronous methods to use UniTask, so you can await them, and pass in a cancellation token instead of a callback.

## Getting Started

This example script shows a simple Asset Register request. It queries the Asset schema, including only the `id` field. After checking that the request has succeeded, the response is parsed for the Asset, and the `id` is logged.

```csharp
public class Example : MonoBehaviour
{
  [SerializeField] private MonoClient _client;
  [SerializeField] private string _collectionId;
  [SerializeField] private string _tokenId;

  private IEnumerator Start()
  {
    IResponse response = null;

    yield return AR.NewQueryBuilder()
      .AddAssetQuery(_collectionId, _tokenId)
        .WithField(asset => asset.Id)
      .Execute(_client, r => response = r);

    if (!response.Success)
    {
      Debug.LogError(response.Error);
      yield break;
    }

    if (response.TryGetResult(out AssetResult result))
    {
      Debug.Log(result.Asset.Id);
    }
  }
}
```

To use this script, add it to a GameObject in your scene. You will also need to add the MonoClient component, and add it to the `Client` field of the `Example` component. 

![image](https://github.com/user-attachments/assets/8f39e359-59f0-4d83-8cc5-00ef9e4db993)

Note that on MonoClient, you can set the environment to either `Staging` or `Production`. This affects the endpoint that the GraphQL requests are sent to. To complete the test, you can use

* Collection ID: `11155111:evm:0x85225575aae6e8275e3d2be9e86268f916f3e2be`
* Token ID: `400`

Enter play mode, and you should see the Asset's ID pop up in the Console window.

> [!TIP]
> You can see the [equivalent request](https://ar-api.futureverse.cloud/graphql?explorerURLState=N4IgJg9gxgrgtgUwHYBcQC4QEcYIE4CeABAIIDOZCKAFACQoQDWyAkmOkQMop4CWSAcwCEAGiK0oEADZSEUFLwhI2HAMLTZ8xcrBCAlEWAAdJESIBDClWoNmOjvSaswYyTLkKlK8W82edBsamZkS8YCZmAL4mkSAiIABu5nzmAEayZBggwGZGeBFERiC2zkUcRUUieQVFvh7abGWFIBX5SLGRQA) in the Futureverse GraphQL Sandbox.

## Breaking It Down

Let's go through the above script step by step, to see exactly how this is working.

### MonoBehaviour
```csharp
public class Example : MonoBehaviour
{
  [SerializeField] private MonoClient _client;
  [SerializeField] private string _collectionId;
  [SerializeField] private string _tokenId;
```
This script is a MonoBehaviour, so you can add it as a component in your scene. It serializes the Collection ID and Token ID, so you can edit those fields directly on the component. It also serializes a MonoClient, which implements the IClient interface. The client is responsible for sending a request and producing a response.

### Building the Query
```csharp
yield return AR.NewQueryBuilder()
  .AddAssetQuery(_collectionId, _tokenId)
    .WithField(asset => asset.Id)
  .Execute(_client, r => response = r);
```
These lines use the RequestBuilder API to create a query of the Asset schema, and then passes that to the client to produce a response.
* `yield return AR.NewQueryBuilder()` Creates a new QueryBuilder object.
* `.AddAssetQuery(_collectionId, _tokenId)` Adds the Asset query to the QueryBuilder. It requires the Collection ID and Token ID, as these inputs are necessary to run the query.
* `.WithField(asset => asset.Id)` Uses an Expression to select the ID field of the Asset, and adds that to the query.
* `.Execute(_client, r => response = r);` Is a shorthand for calling `Build()` on the QueryBuilder to produce a request and then `IClient.SendRequest()`. It takes a callback parameter that is used to set the local `response` variable.

### Checking Success
```csharp
  if (!response.Success)
  {
    Debug.LogError(response.Error);
    yield break;
  }
```
Here we check the Success property of the IResponse object that was generated by the QueryBuilder. If that returns false, we know there were errors with the query. We can then print the Error property of the response to see what went wrong, and break from the function.

### Getting the Asset
```csharp
if (response.TryGetResult(out AssetResult result))
{
  Debug.Log(result.Asset.Id);
}
```
The IResponse object also provides the `TryGetResult<T>` method. Because multiple queries can be added to the QueryBuilder, the response could contain more than one schema, so the `TryGetResult<T>` usage here checks if the response contains an AssetResult (the result of the Asset query), deserializes it into the out parameter, and then uses the Asset object inside the result to print the ID. 

> [!IMPORTANT]
> Because we only added the ID field to the query, all other properties of the `asset` object will be null. Use extra `.WithField()` calls to add more fields

## Request Builder

The Request Builder API allows you to create highly customizable queries and mutations, mirroring the functionality of the [Sandbox](https://ar-api.futureverse.cloud/graphql) from within your Unity project to get the exact information you need.

Under the hood, the Request Builder system builds a query tree structure, with the `IMutationBuilder` or `IQueryBuilder` as the root of the tree. The `Add` methods return an `IMemberSubBuilder`. As we will see below, sub-builders, including `IMemberSubBuilder`, can create other sub-builders, adding new nodes to the tree. Any `IMemberSubBuilder` can call `Done` to return the parent builder, letting you navigate the tree.

`IMemberSubBuilder` takes a generic parameter `TType` that determines the type of member that is being built. In the script above, Adding an `AssetQuery` produces an `IMemberSubBuilder` where `TType` is `Asset`. 

To get started building a Request, use the `AR.NewQuery()` or `AR.NewMutation()` methods. These methods produce `IQueryBuilder` and `IMutationBuilder` objects respectively.

### Queries and Mutations

Both the `IQueryBuilder` and `IMutationBuilder` provide an `Add` method, which takes either an `IQuery` or `IMutation` object as a parameter. This allows you to add multiple queries or mutations to the request. The `AssetQuery` object used in the script above is an example of an `IQuery` implementation.

> [!NOTE]
> You cannot add a mutation to a QueryBuilder and vice versa, as GraphQL only supports sending one type of request at a time.

### Fields

Any `IMemberSubBuilder` can call `.WithField()`. This takes a Function Expression as a parameter, where the parameter of the function is of TType. This allows you to write the expression shown in the script above `a => a.Id`. In this case, `a` is of type `Asset`, which lets you access the Id member. Since the member Id is the what is returned by the function expression, the Request Builder knows to add that field to the GraphQL query.

You can also chain members together in the Expression, e.g. `a => a.Collection.ChainID`. This will add both the Collection, and ChainID of the Collection to the query.

### Members
`IMemberSubBuilder`s have an `OnMember()` method, which lets you avoid long chains of members when using `.WithField()`. `.OnMember()` also takes an expression parameter, and will return an `IMemberSubBuilder` where TType is the type of the member that ends the expression chain. So instead of using a `.WithField()` call with `a => a.Collection.ChainID` as the expression, you can do the following:

```csharp
.OnMember(a => a.Collection)
  .WithField(c => c.ChainID)
  .Done()
```

It is never necessary to use `.OnMember()`, but it is useful when you have many nested fields to add, and can help to make your request builder more readable.

### Arrays
When using `.WithField()`, you will likely come across an array type, and may think to do something like this: `.WithField(a => a.Items[0].Content)`. This will not work, but `IMemberSubBuilder` comes with `.WithArray()` that enables you to add fields from an array in the request. Using this, we can transform the incorrect code into the proper solution:

```csharp
.OnArray(a => a.Items)
  .WithField(i => i.Content)
  .Done()
```

### Methods

An `IMemberSubBuilder` can also call `.OnMethod()`. Some GraphQL queries can call sub-methods within the hierarchy of the query. An example of this is `SFTAssetOwnership`, which has a method called `balanceOf`. `balanceOf` takes a string input is a parameter. Using `.OnMethod()`, you can call `balanceOf` at the end of the method chain, e.g. in a `IMemberSubBuilder` where `TType` is `SFTAssetOwnership`, you can call `.OnMethod(sft => sft.balanceOf("some address"))`.

`.OnMethod()` returns an `IMethodSubBuilder`, where `TType` is the return type of the method call. In the case of `balanceOf`, it is a `SFTBalance` object. This lets you continue adding fields to the query that result from the method call.

> [!CAUTION]
> A runtime exception is thrown if you try to call a method in a `.WithField()` expression. Always use `.OnMethod()` when dealing with method calls.

### Unions

`IMemberSubBuilder`s can also call `.OnUnion()`, which also takes an Expression parameter. See more about GraphQL unions [here](https://graphql.org/learn/schema/#union-types), but essentially, a Union can be one of multiple types. When calling `.OnUnion()`, the member chain must end with a member that implements the `IUnion` interface. `.OnUnion()` returns an `IUnionSubBuilder`.

The `IUnionSubBuilder` Provides a single method: `On<T>()` where the generic parameter `TUnionType` is a subtype of the Union member that `.OnUnion()` was called with. It returns an `IMemberSubBuilder` where `TType` is the `TUnionType`. A concrete example is provided further on.

When getting a Union type from the IResult object, the following syntax is recommended:

```csharp
if (response.TryGetResult<AssetResult>(out var result))
{
  if (result.Asset.Ownership is SFTAssetOwnership ownership)
  {
    Debug.Log(ownership.BalanceOf.Balance);
  }
}
```

### Interfaces

Any `IMemberSubBuilder` can call `.OnInterface()`, which takes an Expression parameter that must end in an interface type that derives from `IInterface`. `.OnInterface()` returns an `IInterfaceSubBuilder` object. GraphQL interface types ([See here](https://graphql.org/learn/schema/#interface-types)) are similar to Unions, but they can have some fields that are common to all implementations. Therefore, `IInterfaceSubBuilder` has all the same methods as `IMemberSubBuilder` (WithField, OnMember, etc.), and also has an `.On<T>()` method similar to the `IUnionSubBuilder`, where `T` must derive from the interface type provided.

### Putting it all together

Below is a more complex example of a query that puts together all the different aspects of the RequestBuilder system.

```csharp
var request = AR.NewQueryBuilder()
  .AddAssetQuery(_collectionId, _tokenId)
    .WithField(a => a.TokenId)
    .OnMember(a => a.Collection)
      .WithField(c => c.ChainType)
      .WithField(c => c.ChainID)
      .Done()
    .OnUnion(a => a.Ownership)
      .On<NFTAssetOwnership>()
        .OnArray<Link, Link[]>(nft => nft.ChildLinks)
          .WithField(l => l.Asset.Id)
          .WithField(l => l.Path)
          .Done()
        .Done()
      .On<SFTAssetOwnership>()
        .OnMethod(sft => sft.balanceOf(_address))
          .WithField(b => b.Balance)
.Build();

IResponse response = null;
yield return _client.SendRequest(request, r => response = r);
```

> [!TIP]
> You can see the [equivalent request](https://ar-api.futureverse.cloud/graphql?explorerURLState=N4IgJg9gxgrgtgUwHYBcQC4QEcYIE4CeABAIIDOZCKAFACQoQDWyAkmOkQMop4CWSAcwCEAGiK0oEADZSEUFLwhI2HAMLTZ8xcrCjxAQzBg8CCmoAW%2B-iSMmKQgJRFgAHSREi%2BilWoNmOjnomVjAxSRk5BSUVcXDNKJ0nV3cPIj8Qt1SiOMjtZ0ys7Mt%2BNgKPAF8yoggAdyR8MnNeAAdkwqIAOi6lIgA5ADEAFXJKFAB5Ooam5vyU9tr6vFn2rMskMFkqrMq51J32ro6eziGRqgnFxpbllYAjfSl9JCgEMYAzakNjUzJAr7syEktoV7o9nghgRVgfs9gUduUQCIQAA3fR8fS3WRkDAgYAeFx4AouEDpHTEjjE4kiAlEkA5LTRMDkoiUxE0lLE-4-Zms9kI8pAA) in the Futureverse GraphQL Sandbox.

Some things to note about this example:
* The `Done()` method is called to return the parent builder, letting you call multiple `.On<>` methods for the `Ownership` union or continue adding fields to the Asset after adding the Collection fields.
* For any sub-builder, calling `.Build()` will simply call `Build` on the parent builder. This means you don't have to chain multiple `Build()` calls at the end of the query, you just call it once. This applies to the `.Execute()` method as well.
* Sometimes, `.OnArray()` cannot infer the type. In this case, like above, you will have to explicitly set the generic parameters of the `.OnArray()` method. They should always be the singular type of the array member, followed by the array type, in this case `<Link, Link[]>`.
* `.Build()` is used here instead of the `.Execute()` shorthand you saw in the first example. This returns an `IRequest` object, which is then passed into the client's `SendRequest()` method.
* `.OnMember()` is not necessary, as we could just as easily call `.WithField(a => a.Collection.ChainType)` and `.WithField(a => a.Collection.ChainID)`, but it helps to organize the request builder and make it more readable.
* Speaking of readable, the indenting you see here does not happen by default, but it is recommended to increase the indent level after each `.On...()` call, and return one indent level after each `.Done()` call. This makes it a lot easier to see the structure of the request clearly.

## More on Requests

### Setting Headers

Given an `IRequest` object, you can set any headers that should be added to the http request like so:

```csharp
request.Headers.Add("Authorization", _authToken);
```

But you can also set them on an `IQueryBuilder` or `IMutationBuilder` directly:

```csharp
yield return AR.NewQueryBuilder()
  // Query
  .SetHeader("Authorization", _authToken)
  .Execute(_client, r => response = r);
```

Setting the `Content-Type` header to `application/json` is done automatically, so you don't need to do that every time. But since setting the auth header will likely be the most common one, you could replace the `SetHeader` call in the above example with the shortcut `.SetAuth(_authToken)`.

### Raw Requests

You can create an `IRequest` object from a raw query string by calling `AR.RawRequest()`, and passing in the request body, an optional variables object, and an optional dictionary of headers. Again, the the `Content-Type` header is set automatically here.

### Caching

Due to its use of reflection, using the RequestBuilder system frequently could be a performance concern. If you are sending the same query multiple times, it is recommended to cache the `IRequest` object that is created by the RequestBuilder, and pass that into `IClient.SendRequest` rather than using the `.Execute()` shorthand.

If you need to send the same request but with different variables, the `IRequest` interface provides an `OverrideInputs` method. This can be used like so:

IResponse response = null;
```csharp
var request = AR.NewQueryBuilder()
  .AddAssetQuery(_collectionId1, _tokenId1)
    .WithField(a => a.TokenId)
  .Build();

yield return _client.SendRequest(request, r => response = r);

request.OverrideInputs(new AssetInput(_collectionId2, _tokenId2));

yield return _client.SendRequest(request, r => response = r);
```

---

## 📄 License

This SDK is released under the [Apache License 2.0](https://www.apache.org/licenses/LICENSE-2.0).
