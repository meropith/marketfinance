# Issues with ProductApplicationService
Services return different result types making it difficult to provide a unified check policy
Everything is in one method.
SellerApplication.cs contains both the implementation of the class and the interface declaration as well. 
The flow of the code is not intuitive had to run debug many times and F10 to understand what is happening.
Changing ProductApplicationService will cause breaks everywhere making this a single point of failure and a strongly couple project. 
To String is environment/culture Dependant
Duplication of code when creating `CompanyDataRequest` and when doing result checks


 
 