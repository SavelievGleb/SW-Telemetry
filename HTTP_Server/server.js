const host = '127.0.0.1'
const httpPort = 25605

const express = require('express')
const app = express()

const fs = require('fs')

const fileName = 'telemetry.txt'

app.listen(httpPort, host, () => {
    console.log(`Listening on port ${httpPort}`)
})

app.get('/write', (req, res) => {
    if (req.url.length > 6) {
        let params = req.url.split('?')[1].split('&')
        let data = ''
        params.forEach((e) => {
            let param = e.split('=')
            data += param[1].replace('.', ',') + '\t'
        })
        data = data.slice(0, -1)
        fs.appendFile(fileName, data + '\n', (err) => { if (err) { console.log(err) } else { console.log(data) } })
    }
    res.statusCode = 200
    res.send()
})

app.get('/clear', (req, res) => {
    fs.writeFile(fileName, '', (err) => { if (err) { console.log(err) } else { console.log('Clear file') } })
    res.statusCode = 200
    res.send()
})