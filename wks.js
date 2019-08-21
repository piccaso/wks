// build Index
let index = [
  {service: "Calendar", keyword: "when", weight: 5},
  {service: "Calendar", keyword: "appointment", weight: 10},
  {service: "Calendar", keyword: "meeting", weight: 10},
  {service: "Wiki", keyword: "vpn", weight: 10},
  {service: "Wiki", keyword: "toilet", weight: 5},
  {service: "Wiki", keyword: "intranet", weight: 10},
]

function search(question){
  let scores = {}, query = question.toLowerCase();
  // build scores from question and index
  index.forEach(i => {
    if(query.indexOf(i.keyword) === -1) return;
    // add weight to the services score
    let score = scores[i.service] || 0;
    scores[i.service] = score + i.weight;
  });
  return scores;
}

console.log(search("Can i use vpn on the toilet to check my mails?"));
// -> {Wiki: 15}

console.log(search("When is my meeting about the new toilets?"));
// -> {Calendar: 15, Wiki: 5}
