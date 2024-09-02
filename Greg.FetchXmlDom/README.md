# Greg.FetchXmlDom

A simple library that provides an object-oriented way to create FetchXml expressions for PowerApps / Dynamics 365.

Many users (me included) prefer QueryExpressions to FetchXml because they are strongly typed and easy to work with, thanks to intellisense and compile time checks. 
FetchXml is a bit more cumbersome, you need to manually manipulate XML files, and this may lead to unwanted errors at runtime. 

At the same time, FetchXml is more powerful than QueryExpressions, and it is the only way to perform some kind of operations like aggregations, grouping, and complex joins.

This library provides a way to create FetchXml expressions in a strongly typed way.