import fetch from 'node-fetch';
class CommonApiFunctions
{

    async executePostRequest(endPoint ,body, headers = {'Content-Type': 'application/json'})
    {
        console.log("*****************") ;
        console.log("Request Body : " + await JSON.stringify(body)) ;


        const response = await fetch(endPoint, {
            method: 'POST',
            body: JSON.stringify(body),
            headers: headers
          });
          
           console.log("Response : " + await response.clone().text()) ; // body used already for error is resolved

          console.log("*****************") ;
          return  response;
    }

    async executeGetRequest(endPoint,headers)
    {
      const response = await fetch(endPoint, {
        method: 'GET',
        headers: headers
      });
    }

}
export default new CommonApiFunctions;
