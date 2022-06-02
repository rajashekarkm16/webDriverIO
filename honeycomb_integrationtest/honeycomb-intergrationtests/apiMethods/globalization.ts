 class Globalization
{
    async getGlobalizationQuery(tenantId :number, url :string)
    {
        const query = [
            {
            "query": `query Globalisation($tenantId: Int!, $url: String!) {
                globalisation(tenantId: $tenantId, url: $url) {
                    availableContext {
                    domainId
                    tenantId
                    currencyCode
                    cultureCode
                    }
                    currentContext {
                    cultureCode
                    currencyCode
                    domainId
                    posId
                    tenantId
                    }
                }
                }`,
            "variables": `{
                        "tenantId": ${tenantId},
                        "url": "${url}"
                    }`
            }
        ]
        return await query;   
    }
}

export default new Globalization;
