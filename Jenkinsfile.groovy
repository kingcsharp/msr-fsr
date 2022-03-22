
library identifier: 'build-configs@master',

        retriever: modernSCM([$class: 'GitSCMSource', credentialsId: 'b7bf4905-1bd1-47f1-8ea3-2478c05e2622',
                              remote: 'https://github.com/CMHWorks/build-configs.git',
                              traits: [[$class: 'WipeWorkspaceTrait']]])

build("answer")