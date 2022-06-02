 class SelectRooms
{
    async getSelectRoomsQuery(estabId, tripStateId , roomBoardId)
    {
      const selectRoomsQuery = [
        {
          "query": `mutation SelectRooms($request: SelectRoomsRequest!) {
      selectRooms(request: $request) {
        tripStateId
      }
    }`,
          "variables": `{
        "request": {
          "estabId": ${estabId},
        "roomIds":"${roomBoardId}",
          "tripStateId": "${tripStateId}"
        }
      }`
        }]
        return selectRoomsQuery;   
    }
}

export default new SelectRooms;
