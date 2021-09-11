function timestamp(str) {
  let dateObj = new Date(str)
  let timeVal = Math.round(dateObj.getTime()) / 1000
  if (isNaN(timeVal)) {
    throw "illegal timestamp format"
  } else {
    return timeVal
  }
}

function time(str) {
  return ({
    time_stamp: timestamp(str),
    time_string: str
  })
}
